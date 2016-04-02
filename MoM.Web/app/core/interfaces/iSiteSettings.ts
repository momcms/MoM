export interface SiteSettings {
    Title: string;
    Theme: Theme;
    Authentication: Authentication;
    IsInstalled: boolean;
    Logo: Logo;
    ModulePath: string;
    ConnectionString: string;
    Email: Email;
}
export interface Theme {
    Module: string;
    Selected: string;
}
export interface Authentication {
    Facebook: Facebook;
    Google: Google;
    Microsoft: Microsoft;
    Twitter: Twitter;
}
export interface Facebook {
    AppId: string;
    AppSecret: string;
    Enabled: boolean;
}
export interface Google {
    ClientId: string;
    ClientSecret: string;
    Enabled: boolean;
}
export interface Microsoft {
    ClientId: string;
    ClientSecret: string;
    Enabled: boolean;
}
export interface Twitter {
    ConsumerKey: string;
    ConsumerSecret: string;
    Enabled: boolean;
}
export interface Logo {
    Height: number;
    ImagePath: string;
    SvgPath: string;
    UseImageLogo: boolean;
    UseSvgLogo: boolean;
    Width: number;
}
export interface Email {
    HostName: string;
    Password: string;
    Port: number;
    RequireCredentials: boolean;
    SenderEmailAdress: string;
    UserName: string;
    UseSSL: boolean;
}