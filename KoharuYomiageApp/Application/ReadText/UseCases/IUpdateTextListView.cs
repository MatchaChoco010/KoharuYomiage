using System;
using System.Collections.Generic;

namespace KoharuYomiageApp.Application.ReadText.UseCases
{
    public interface IUpdateTextListView
    {
        void Update(IEnumerable<(Guid, string)> list);
    }
}
