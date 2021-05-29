using System;
using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.UpdateTextList
{
    public interface IUpdateTextListView
    {
        void UpdateTextList(IEnumerable<(Guid, string)> list);
    }
}
