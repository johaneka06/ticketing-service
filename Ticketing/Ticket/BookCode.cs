using System;
using System.Text;

namespace Ticketing
{
    public class BookCode
    {
        private string _code;

        public string Code
        {
            get
            {
                return this._code;
            }
        }

        public BookCode(string code)
        {
            _code = code;
        }

        public BookCode()
        {
            _code = codeFiller();
        }

        private string codeFiller()
        {
            string CodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            var code = new StringBuilder();

            for(int i = 0; i < 6; i++)
            {
                code.Append(CodeChars[rand.Next(CodeChars.Length)]);
            }

            return code.ToString();
        }

        public override bool Equals(object obj)
        {
            var o = obj as BookCode;
            if(o == null) return false;

            return this._code == o._code;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}