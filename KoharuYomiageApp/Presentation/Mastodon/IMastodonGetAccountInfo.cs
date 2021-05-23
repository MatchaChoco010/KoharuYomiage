﻿using System;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public interface IMastodonGetAccountInfo
    {
        Task<(string, Uri)> GetAccountInfo(string instance, string accessToken);
    }
}