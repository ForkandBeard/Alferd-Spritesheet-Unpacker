using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Logic.BAL
{
    public interface ISaveLoadable
    {
        Guid GetId();
        string GetSubDirectoryPath();
        int GetIndex();
        string GetUniqueCode();
    }
}
