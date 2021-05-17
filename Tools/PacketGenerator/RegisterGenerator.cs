using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativePacketGenerator
{
    /// <summary>
    /// パケットをカスタムクラスとして登録するメソッドを生成するクラス
    /// </summary>
    public class RegisterGenerator
    {
        /// <summary>
        /// テンプレートファイル名
        /// </summary>
        private static readonly string TemplateFilePath = "templates/Register.txt";

        private static readonly string DestFilePath = "../Common/Sources/Packet/RegisterPacketType.cs";

        /// <summary>
        /// クラス群
        /// </summary>
        private List<ClassData> Classes = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Classes">クラス群</param>
        public RegisterGenerator(List<ClassData> Classes)
        {
            this.Classes = Classes;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <return>成功したらtrue</return>
        public bool Generate()
        {
            try
            {
                StreamReader Reader = new StreamReader(TemplateFilePath, Encoding.GetEncoding("UTF-8"));
                string Template = Reader.ReadToEnd();
                Reader.Close();

                string Methods = "";
                byte Code = 1;
                foreach (var Class in Classes)
                {
                    if (!string.IsNullOrEmpty(Class.PacketID)) { continue; }
                    Methods += ("\t\t\tMethod?.Invoke(typeof($CLASS_NAME$), " + Code + ", $CLASS_NAME$.SerializeObject, $CLASS_NAME$.DeserializeObject);\n").Replace("$CLASS_NAME$", Class.ClassName);
                    Code++;
                }
                Template = Template.Replace("$REGISTER$", Methods);

                using (StreamWriter Writer = new StreamWriter(DestFilePath, false, Encoding.GetEncoding("UTF-8")))
                {
                    Writer.Write(Template);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
