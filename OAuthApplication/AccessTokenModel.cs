﻿namespace OAuthApplication;

public class AccessTokenModel
{
    public string access_token { get; set;}
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public int ext_expires_in { get; set; }
}
