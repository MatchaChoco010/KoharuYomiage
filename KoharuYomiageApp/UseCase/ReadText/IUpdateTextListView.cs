using System;
using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.ReadText
{
    public interface IUpdateTextListView
    {
        void UpdateTextList(IEnumerable<(Guid, string)> list);
    }
}
