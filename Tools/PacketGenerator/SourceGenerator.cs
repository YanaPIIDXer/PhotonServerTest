using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NativePacketGenerator
{
    /// <summary>
    /// ソース生成.
    /// </summary>
    public class SourceGenerator
    {

        /// <summary>
        /// クラスデータ
        /// </summary>
        private ClassData Class = null;

        /// <summary>
        /// テンプレートファイル名.
        /// </summary>
        private static readonly string TemplateFileName = "templates\\Template.txt";

        /// <summary>
        /// コンストラクタテンプレート名.
        /// </summary>
        private static readonly string ConstructorTemplateFileName = "templates\\Constructor.txt";

        /// <summary>
        /// 置換結果.
        /// </summary>
        private string Result = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="InClass">クラスデータ</param>
        /// <param name="InOutputPath">出力先</param>
        public SourceGenerator(ClassData InClass)
        {
            Class = InClass;
        }

        /// <summary>
        /// 生成.
        /// </summary>
        /// <returns>成功したらtrue</returns>
        public bool Generate()
        {
            try
            {
                string Template = ReadFromFile(TemplateFileName);
                Result = ParseTag(Template);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ファイルから読み込み
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <returns>読み込んだテキスト</returns>
        private string ReadFromFile(string FileName)
        {
            StreamReader Reader = new StreamReader(FileName, Encoding.GetEncoding("UTF-8"));
            string Result = Reader.ReadToEnd();
            Reader.Close();
            return Result;
        }

        /// <summary>
        /// 書き出し
        /// </summary>
        /// <param name="TargetPath">パス</param>
        /// <returns>成功したらtrue</returns>
        public bool Write(string TargetPath)
        {
            var OutputPath = TargetPath + "\\" + Class.ClassName + ".cs";
            try
            {
                string LoadedSrc = "";
                if (File.Exists(OutputPath))
                {
                    using (StreamReader Reader = new StreamReader(OutputPath, Encoding.GetEncoding("UTF-8")))
                    {
                        LoadedSrc = Reader.ReadToEnd();
                    }
                }

                // ファイル名はここで置換.
                Result = Result.Replace("$FILE_NAME$", Path.GetFileName(OutputPath));

                if (LoadedSrc == Result) { return true; }

                using (StreamWriter Writer = new StreamWriter(OutputPath, false, Encoding.GetEncoding("UTF-8")))
                {
                    Writer.Write(Result);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// タグ解析.
        /// </summary>
        /// <param name="Template">テンプレート</param>
        /// <returns>タグ解析結果</returns>
        private string ParseTag(string Template)
        {
            // コンストラクタ。
            // メンバが無ければ消滅する。
            string Constructor = "";
            if (Class.Members.Count > 0)
            {
                Constructor = ReadFromFile(ConstructorTemplateFileName);
            }
            Template = Template.Replace("$CONSTRUCTOR$", Constructor);

            // クラスコメント
            Template = Template.Replace("$CLASS_COMMENT$", Class.Comment);

            // クラス名.
            Template = Template.Replace("$CLASS_NAME$", Class.ClassName);

            // 基底クラス
            string BaseClass = "";
            if (Class.PacketID != null)
            {
                BaseClass = " : IPacket";
            }
            else
            {
                BaseClass = " : ISerializable";
            }
            Template = Template.Replace("%BASE_CLASS%", BaseClass);

            // パケットID
            string PacketId = "";
            if (Class.PacketID != null)
            {
                PacketId = "public EPacketID PacketID { get { return EPacketID." + Class.PacketID + "; } }";
            }
            Template = Template.Replace("$PACKET_ID$", PacketId);

            // enum
            string Enums = "";
            foreach (var KeyValue in Class.EnumList)
            {
                Enums += "enum " + KeyValue.Key + "\n\t{\n";
                foreach (var Data in Class.EnumList[KeyValue.Key])
                {
                    Enums += "\t\t//! " + Data.Comment + "\n";
                    Enums += "\t\t" + Data.Name;
                    Enums += ",\n";
                }
                Enums += "\t};";
            }
            Template = Template.Replace("$ENUMS$", Enums);

            // メンバ変数.
            string Members = "";
            for (int i = 0; i < Class.Members.Count; i++)
            {
                var MemberData = Class.Members[i];
                Members += "/// <summary>\n\t\t///  " + MemberData.Comment + "\n\t\t/// </summary>\n\t\t";
                Members += "public " + MemberData.TypeName + " " + MemberData.Name + " = new " + MemberData.TypeName + "();\n\n\t\t";
            }
            Template = Template.Replace("$MEMBERS$", Members);

            // コンストラクタのメンバ
            string ConstructorMembers = "";
            if (Class.Members.Count > 0)
            {
                for (int i = 0; i < Class.Members.Count - 1; i++)
                {
                    var MemberData = Class.Members[i];
                    ConstructorMembers += MemberData.TypeName + " " + MemberData.Name + ", ";
                }
                ConstructorMembers += Class.Members.Last().TypeName + " " + Class.Members.Last().Name;
            }
            Template = Template.Replace("$CONSTRUCTOR_MEMBERS$", ConstructorMembers);

            // メンバを押し込む
            string PutMembers = "";
            for (int i = 0; i < Class.Members.Count; i++)
            {
                PutMembers += "this." + Class.Members[i].Name + " = " + Class.Members[i].Name + ";\n\t\t\t";
            }
            Template = Template.Replace("$PUT_MEMBERS$", PutMembers);

            string SerializeFunctions = "";
            for (int i = 0; i < Class.Members.Count; i++)
            {
                var Member = Class.Members[i];
                if (Member.IsPrimitive)
                {
                    SerializeFunctions += "Stream.Serialize(ref " + Member.Name + ");\n\t\t\t";
                }
                else
                {
                    SerializeFunctions += Member.Name + ".Serialize(Stream);\n\t\t\t";
                }
            }
            Template = Template.Replace("$SERIALIZE_MEMBERS$", SerializeFunctions);

            return Template;
        }

    }
}
