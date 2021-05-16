using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NativePacketGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var Excels = Directory.GetFiles("PacketData");
            List<ClassData> Classes = new List<ClassData>();
            foreach (var Excel in Excels)
            {
                ExcelParser Parser = new ExcelParser(Excel);
                if (!Parser.Parse())
                {
                    Console.Error.WriteLine(Excel + "のパースに失敗しました。");
                    return;
                }
                foreach (var Class in Parser.Classes)
                {
                    Classes.Add(Class);
                }
            }

            IDEnumGenerator IDGen = new IDEnumGenerator(Classes);
            if (!IDGen.Generate(0, "PacketID")) { return; }
            string SrcPath = "../Common/Sources/Packet/";
            if (!IDGen.Write(SrcPath + "PacketID.cs")) { return; }

            foreach (var Class in Classes)
            {
                SourceGenerator Gen = new SourceGenerator(Class);
                Console.WriteLine(Class.ClassName + "の出力中・・・");
                if (!Gen.Generate())
                {
                    Console.WriteLine("ソースコードの生成に失敗しました。");
                    return;
                }

                if (!Gen.Write(SrcPath))
                {
                    Console.WriteLine("ソースコード書き込みに失敗しました。");
                    return;
                }
            }

            RegisterGenerator RegisterGen = new RegisterGenerator(Classes);
            if (!RegisterGen.Generate())
            {
                Console.WriteLine("カスタムクラスレジスタのソースコード生成に失敗しました。");
                return;
            }
        }
    }
}
