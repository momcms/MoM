import {SiteSettingThemeDto} from "./SiteSettingThemeDto";
import {SiteSettingAuthenticationDto} from "./SiteSettingAuthenticationDto";
import {SiteSettingLogoDto} from "./SiteSettingLogoDto";
import {SiteSettingEmailDto} from "./SiteSettingEmailDto";

export interface SiteSettingDto {
    title: string;
    theme: SiteSettingThemeDto;
    authentication: SiteSettingAuthenticationDto;
    isInstalled: boolean;
    logo: SiteSettingLogoDto;
    modulePath: string;
    email: SiteSettingEmailDto;
}
