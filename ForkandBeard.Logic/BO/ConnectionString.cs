using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Logic.BO
{
    public class ConnectionString : BAL.ISaveLoadable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsProduction { get; set; }
        public bool IsEnabled { get; set; }
        public int Index { get; set; }

        public ConnectionString()
        {
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public Guid GetId()
        {
            return this.Id;
        }

        public string GetSubDirectoryPath()
        {
            return BAL.Enums.Directory.Connection_Strings.ToString();
        }

        public int GetIndex()
        {
            return this.Index;
        }

        public string GetUniqueCode()
        {
            return this.Name;
        }
    }
}
