using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace N88Basic
{
    public class N88Basic
    {
        private readonly string[] rsvdword =
        {
            "",            "ＥＮＤ",         "ＦＯＲ",         "ＮＥＸＴ",
            "ＤＡＴＡ",         "ＩＮＰＵＴ",      "ＤＩＭ",         "ＲＥＡＤ",
            "ＬＥＴ",         "ＧＯＴＯ",         "ＲＵＮ",         "ＩＦ",
            "ＲＥＳＴＯＲＥ",      "ＧＯＳＵＢ",      "ＲＥＴＵＲＮ",      "ＲＥＭ",
            "ＳＴＯＰ",         "ＰＲＩＮＴ",         "ＣＬＥＡＲ",      "ＬＩＳＴ",
            "ＮＥＷ",         "ＯＮ",         "ＷＡＩＴ",         "ＤＥＦ",
            "ＰＯＫＥ",         "ＣＯＮＴ",         "ＯＵＴ",         "ＬＰＲＩＮＴ",
            "ＬＬＩＳＴ",         "ＣＯＮＳＯＬＥ",      "ＷＩＤＴＨ",      "ＥＬＳＥ",
            "ＴＲＯＮ",         "ＴＲＯＦＦ",      "ＳＷＡＰ",      "ＥＲＡＳＥ",
            "ＥＤＩＴ",         "ＥＲＲＯＲ",      "ＲＥＳＵＭＥ",      "ＤＥＬＥＴＥ",
            "ＡＵＴＯ",         "ＲＥＮＵＭ",      "ＤＥＦＳＴＲ",      "ＤＥＦＩＮＴ",
            "ＤＥＦＳＮＧ",      "ＤＥＦＤＢＬ",      "ＬＩＮＥ",         "ＷＨＩＬＥ",
            "ＷＥＮＤ",      "ＣＡＬＬ",         "",            "",
            "",            "ＷＲＩＴＥ",      "ＣＯＭＭＯＮ",      "ＣＨＡＩＮ",
            "ＯＰＴＩＯＮ",      "ＲＡＮＤＯＭＩＺＥ",   "ＤＳＫＯ＄",      "ＯＰＥＮ",
            "ＦＩＥＬＤ",         "ＧＥＴ",         "ＰＵＴ",         "ＳＥＴ",
            "ＣＬＯＳＥ",      "ＬＯＡＤ",         "ＭＥＲＧＥ",      "ＦＩＬＥＳ",
            "ＮＡＭＥ",      "ＫＩＬＬ",         "ＬＳＥＴ",         "ＲＳＥＴ",
            "ＳＡＶＥ",         "ＬＦＩＬＥＳ",      "ＭＯＮ",         "ＣＯＬＯＲ",
            "ＣＩＲＣＬＥ",      "ＣＯＰＹ",         "ＣＬＳ",         "ＰＳＥＴ",
            "ＰＲＥＳＥＴ",      "ＰＡＩＮＴ",         "ＴＥＲＭ",         "ＳＣＲＥＥＮ",
            "ＢＬＯＡＤ",      "ＢＳＡＶＥ",      "ＬＯＣＡＴＥ",      "ＢＥＥＰ",
            "ＲＯＬＬ",         "ＨＥＬＰ",         "",            "ＫＡＮＪＩ",
            "ＴＯ",         "ＴＨＥＮ",         "ＴＡＢ（",         "ＳＴＥＰ",
            "ＵＳＲ",         "ＦＮ",         "ＳＰＣ（",         "ＮＯＴ",
            "ＥＲＬ",         "ＥＲＲ",         "ＳＴＲＩＮＧ＄",      "ＵＳＩＮＧ",
            "ＩＮＳＴＲ",         "＇",         "ＶＡＲＰＴＲ",      "ＡＴＴＲ＄",
            "ＤＳＫＩ＄",      "ＳＲＱ",         "ＯＦＦ",         "ＩＮＫＥＹ＄",
            ">",         "＝",         "<",         "＋",
            "－",         "＊",         "／",         "^",
            "ＡＮＤ",         "ＯＲ",         "ＸＯＲ",         "ＥＱＶ",
            "ＩＭＰ",         "ＭＯＤ",         "\\",         "",
            "",            "ＬＥＦＴ＄",      "ＲＩＧＨＴ＄",      "ＭＩＤ＄",
            "ＳＧＮ",         "ＩＮＴ",         "ＡＢＳ",         "ＳＱＲ",
            "ＲＮＤ",         "ＳＩＮ",         "ＬＯＧ",         "ＥＸＰ",
            "ＣＯＳ",         "ＴＡＮ",         "ＡＴＮ",         "ＦＲＥ",
            "ＩＮＰ",         "ＰＯＳ",         "ＬＥＮ",         "ＳＴＲ＄",
            "ＶＡＬ",         "ＡＳＣ",         "ＣＨＲ＄",      "ＰＥＥＫ",
            "ＳＰＡＣＥ＄",      "ＯＣＴ＄",         "ＨＥＸ＄",      "ＬＰＯＳ",
            "ＣＩＮＴ",         "ＣＳＮＧ",         "ＣＤＢＬ",         "ＦＩＸ",
            "ＣＶＩ",         "ＣＶＳ",         "ＣＶＤ",         "ＥＯＦ",
            "ＬＯＣ",         "ＬＯＦ",         "ＦＰＯＳ",         "ＭＫＩ＄",
            "ＭＫＳ＄",      "ＭＫＤ＄",      "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "ＤＳＫＦ",         "ＶＩＥＷ",         "ＷＩＮＤＯＷ",      "ＰＯＩＮＴ",
            "ＣＳＲＬＩＮ",      "ＭＡＰ",         "ＳＥＡＲＣＨ",      "ＭＯＴＯＲ",
            "ＰＥＮ",         "ＤＡＴＥ＄",      "ＣＯＭ",         "ＫＥＹ",
            "ＴＩＭＥ＄",      "ＷＢＹＴＥ",      "ＲＢＹＴＥ",      "ＰＯＬＬ",
            "ＩＳＥＴ",         "ＩＥＥＥ",         "ＩＲＥＳＥＴ",      "ＳＴＡＴＵＳ",
            "ＣＭＤ",         "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
            "",            "",            "",            "",
        };

        private readonly Dictionary<byte, string> lookup;

        private const byte ENDLINE = 0x00;
        private const byte CMD_PRINTSTIRNG = 0x3A;

        public N88Basic(string tableFilePath)
        {
            lookup = new();

            var fileStream = File.OpenRead(tableFilePath);
            var streamReader = new StreamReader(fileStream, Encoding.UTF8, true);

            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var split = line.Split("=", 2);

                if (split.Length != 2)
                {
                    throw new Exception($"Malformed line: {line}");
                }

                if (split[1] == "")
                {
                    continue;
                }

                lookup.Add(Convert.ToByte(split[0], 16), split[1]);
            }
        }

        public string Disassembler(string fileName)
        {
            // https://github.com/rururutan/m88/blob/1c48d83070202eef43ab00db757131d0cc4768cc/src/win32/basmon.cpp
            var stream = File.Open(fileName, FileMode.Open);
            var reader = new BinaryReader(stream, Encoding.ASCII, false);

            StringBuilder text = new();

            while (true)
            {
                var link = reader.ReadUInt16();

                if (link <= 0x0)
                {
                    break;
                }

                if (text.Length != 0)
                {
                    text.Append('\n');
                }

                var lineNumber = string.Format($"{reader.ReadUInt16():00000}: ");

                text.Append(lineNumber);

                while (true)
                {
                    if (stream.Position >= 0x35)
                    {

                    }

                    var c = reader.ReadByte();

                    if (c == 0x0)
                    {
                        break;
                    }

                    switch (c)
                    {
                        case 0x0b:
                            throw new NotImplementedException();
                        case 0x0c:
                            text.Append(string.Format($"0x{reader.ReadUInt16():X4}"));
                            break;
                        case 0x0d:
                            throw new NotImplementedException();
                        case 0x0e:
                            text.Append(string.Format($"0d{reader.ReadUInt16():00000}"));
                            break;
                        case 0x1c:
                            text.Append(string.Format($"0dd{reader.ReadUInt16():00000}"));
                            break;
                        case 0x0f:
                            text.Append(string.Format($"0d{reader.ReadByte():00}"));
                            break;
                        case >= 0x11 and <= 0x1b:
                            text.Append(string.Format($"0c{c - 0x11}"));
                            break;
                        case 0x1d:
                            long f = reader.ReadUInt32();
                            long ma = (f & 0xffffff) | 0x800000;
                            if ((f & 0x800000) == 0x0)
                            {
                                ma *= -1;
                            }

                            var x = Math.Pow(ma / 0x800000, ((f >> 24) & 0xff) - 129);
                            text.Append(string.Format($"0f{x}"));

                            break;
                        case 0x1f:
                            /*{
                                uint ma1 = reader.ReadUInt32();
                                uint ff = reader.ReadUInt32();
                                var ma2 = (ff & 0xffffff) | 0x800000;

                                double ma3 = (ma2 + ma1 / (65536f * 65536f)) / 0x800000;

                                if ((ff & 0x800000) == 0x0)
                                {
                                    ma3 = -ma3;
                                }

                                var xx = Math.Pow(ma3, ((ff >> 24) & 0xff) - 129);

                                text.Append(string.Format($"0a{xx}"));
                            
                            }*/
                            throw new NotImplementedException();
                            break;
                        case 0x84:
                            text.Append("ＤＡＴＡ");

                            while ((c = reader.ReadByte()) != ':' && c != ENDLINE)
                            {
                                if (c != 0x22)
                                {
                                    text.Append(HexToString(c));
                                    continue;
                                }

                                do
                                {
                                    text.Append(HexToString(c));
                                    c = reader.ReadByte();
                                }
                                while (c != 0x22 && c != ENDLINE);
                            }
                            stream.Position--;
                            break;
                        case 0x22:
                            do
                            {
                                text.Append(HexToString(c));
                                c = reader.ReadByte();
                            }
                            while (c != 0x22);

                            if (c == 0x22)
                            {
                                text.Append(HexToString(0x22));
                            }
                            else
                            {
                                stream.Position--;
                            }
                            break;
                        case CMD_PRINTSTIRNG:
                            var bb = reader.ReadByte();
                            stream.Position--;

                            switch (bb)
                            {
                                case 0x9f:
                                    break;
                                case 0x8f:
                                    stream.Position += 2;

                                    text.Append('\'');

                                    while (reader.PeekChar() != ENDLINE)
                                    {
                                        text.Append(HexToString(reader.ReadByte()));
                                    }

                                    break;
                                default:
                                    var outty = HexToString(c);
                                    text.Append(outty);
                                    break;
                            }

                            break;
                        case 0x8f:
                            /*text.Append(rsvdword[c & 0x7f]);
                            while (reader.PeekChar() != ENDLINE)
                            {
                                text.Append(HexToString(reader.ReadByte()));
                            }
                            break;*/
                            throw new NotImplementedException();
                        case 0xaf:
                            /*text.Append("WHILE");
                            if (reader.PeekChar() == 0xf3)
                            {
                                reader.ReadByte();
                            }
                            break;*/
                            throw new NotImplementedException();
                        default:
                            if (c < 0x80)
                            {
                                text.Append(HexToString(c));
                                break;
                            }

                            string inst;
                            if (c < 0xff)
                            {
                                inst = rsvdword[c & 0x7f];
                            }
                            else
                            {
                                var b = reader.ReadByte() | 0x80;
                                inst = rsvdword[b];
                            }

                            if (inst == "")
                            {
                                inst = "{" + $"{c:X2}" + "}";
                            }

                            text.Append(inst);

                            break;
                    }
                }
            }

            return text.ToString();
        }

        private class BasicLine
        {
            public BasicLine()
            {
                Data = new();
            }

            public ushort PreviousLineLocation { get; set; }
            public ushort CurrentLine { get; set; }
            public List<byte> Data { get; set; }

            public ushort NextLine()
            {
                return (ushort)(Data.Count + 4);
            }

            public byte[] GenerateBytes()
            {
                byte[] b = Array.Empty<byte>();
                b = b.Concat(BitConverter.GetBytes(PreviousLineLocation)).ToArray();
                b = b.Concat(BitConverter.GetBytes(CurrentLine)).ToArray();
                b = b.Concat(Data).ToArray();
                return b;
            }
        }

        public byte[] Assembler(string fileName)
        {
            var biggestKeywordLength = rsvdword.Max(x => x.Length);

            List<byte> b = new();

            var stream = File.Open(fileName, FileMode.Open);
            var reader = new StreamReader(stream, Encoding.UTF8, false);

            var line = "";
            ushort previousLine = 0;

            var previousCommand = "";

            while ((line = reader.ReadLine()) != null)
            {
                var split = line.Split(": ", 2);

                if (split.Length != 2)
                {
                    throw new Exception($"Malformed line: {line}");
                }

                var bl = new BasicLine
                {
                    CurrentLine = ushort.Parse(split[0]),
                };

                string s = split[1];

                while (s != "")
                {
                    if (b.Count + bl.Data.Count >= 0x35)
                    {

                    }

                    switch (s)
                    {
                        case string a when a.StartsWith("0f"):
                            {
                                // MSB == negativity
                                // NBBBBBBBAAAAAAAA AAAAAAAA AAAAAAAA
                                // N = negativity
                                // B = power
                                // A = Base

                                // Not properly implemented to support huge numbers.
                                var extractedNumber = new Regex("^[-0-9]+").Match(s.Replace("0f", "")).Value;
                                var p = int.Parse(extractedNumber);
                                var negativity = p < 0 ? 1 : 0;
                                var result = (Math.Abs(p + 1) & 0xffffff) | (negativity << 31);
                                bl.Data.Add(0x1d);
                                bl.Data = bl.Data.Concat(BitConverter.GetBytes(result)).ToList();
                                s = s[(extractedNumber.Length + 2)..];
                            }
                            break;
                        case string a when new Regex("^\\{[0-9A-Za-z]{2}\\}").IsMatch(a):
                            var h = Convert.ToByte(s.Substring(1, 2), 16);
                            bl.Data.Add(h);
                            s = s[4..];
                            break;
                        case string a when a.StartsWith("'"):
                            bl.Data.Add(CMD_PRINTSTIRNG);

                            s = s[1..];

                            bl.Data.Add(0x8F);
                            bl.Data.Add(0xE9);

                            foreach (var c in s)
                            {
                                bl.Data.Add(StringToHex(c));
                            }

                            s = "";

                            break;
                        case string a when a.StartsWith("ＤＡＴＡ"):
                            bl.Data.Add(0x84);

                            s = s["ＤＡＴＡ".Length..];
                            foreach (var c in s)
                            {
                                bl.Data.Add(StringToHex(c));
                            }
                            s = "";
                            break;
                        case string a when a.StartsWith("0x"):
                            s = s[2..];

                            bl.Data.Add(0x0C);

                            var rawAddress = s.Substring(0, 4);
                            var address = Convert.ToUInt16(rawAddress, 16);

                            bl.Data = bl.Data.Concat(BitConverter.GetBytes(address)).ToList();

                            s = s[4..];
                            break;

                        case string a when a.StartsWith("0c"):
                            {
                                var bbb = (byte)(s[2..][0] - 0x1F);
                                bl.Data.Add(bbb);
                                s = s[3..];
                                break;
                            }
                        case string a when a.StartsWith("0dd"):
                            {
                                var extractedNumber = new Regex("^[0-9]+").Match(s.Replace("0dd", "")).Value;
                                bl.Data.Add(0x1C);
                                var bytes = BitConverter.GetBytes(ushort.Parse(extractedNumber));
                                bl.Data = bl.Data.Concat(bytes).ToList();

                                s = s[(extractedNumber.Length + 3)..];
                                break;
                            }
                        case string a when a.StartsWith("0d"):
                            {
                                var extractedNumber = new Regex("^[0-9]+").Match(s.Replace("0d", "")).Value;

                                switch (extractedNumber.Replace("0d", "").Length)
                                {
                                    case 1:
                                        {
                                            bl.Data.Add((byte)(s[0] - 0x1B));
                                            break;
                                        }
                                    case 5:
                                        {
                                            bl.Data.Add(0x0E);
                                            var bytes = BitConverter.GetBytes(ushort.Parse(extractedNumber));
                                            bl.Data = bl.Data.Concat(bytes).ToList();
                                            break;
                                        }
                                    default:
                                        {
                                            bl.Data.Add(0x0F);
                                            bl.Data = bl.Data.Concat(new byte[] { byte.Parse(extractedNumber) }).ToList();
                                            break;
                                        }
                                }

                                s = s[(extractedNumber.Length + 2)..];
                            }
                            break;


                        default:

                            var foundWord = "";

                            if (true)
                            {
                                if (true)
                                {
                                    if (true)
                                    {
                                        var match = new Regex(@"^([^\s:]+)").Match(s).Value;

                                        for (int sizeAttempt = Math.Min(biggestKeywordLength, s.Length); sizeAttempt > 0; sizeAttempt--)
                                        {
                                            var check = s.Substring(0, sizeAttempt);

                                            if (!rsvdword.Contains(check))
                                            {
                                                continue;
                                            }

                                            foundWord = check;
                                            s = s[sizeAttempt..];
                                            break;
                                        }
                                    }
                                }
                            }

                            if (foundWord == "")
                            {
                                bl.Data.Add(StringToHex(s[0]));
                                s = s[1..];
                                break;
                            }
                            else

                                previousCommand = foundWord;

                            if (foundWord == "ＥＬＳＥ")
                            {
                                bl.Data.Add(0x3A);
                            }

                            byte hexVal = (byte)Array.FindIndex(rsvdword, row => row == foundWord);

                            if (hexVal < 0x80)
                            {
                                bl.Data.Add((byte)(hexVal | 0x80));
                            }
                            else if (hexVal == 0x80)
                            {
                                bl.Data.Add((byte)(hexVal & 0x7f));
                            }
                            else
                            {
                                bl.Data.Add(0xFF);
                                bl.Data.Add(hexVal);
                            }

                            break;
                    }
                }

                bl.Data.Add(ENDLINE);
                bl.PreviousLineLocation = (ushort)((previousLine += bl.NextLine()) + 1);
                b = b.Concat(bl.GenerateBytes()).ToList();
            }

            {
                var bl = new BasicLine
                {
                    CurrentLine = 0x0,
                    PreviousLineLocation = 0x0,
                };

                b = b.Concat(bl.GenerateBytes()).ToList();
            }

            return b.ToArray();
        }

        private string HexToString(byte b)
        {
            if (!lookup.ContainsKey(b))
            {
                Debug.WriteLine($"Key not found: {b:X2}");
                return "{" + string.Format($"{b:X2}") + "}";
            }

            return lookup[b];
        }

        private byte StringToHex(char s)
        {
            return StringToHex(s.ToString());
        }

        private byte StringToHex(string s)
        {
            if (!lookup.ContainsValue(s))
            {
                throw new Exception($"Value not found: {s:X2}");
            }

            return lookup.FirstOrDefault(x => x.Value == s).Key;
        }
    }
}
