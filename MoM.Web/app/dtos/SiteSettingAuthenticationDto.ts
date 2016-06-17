import {SiteSettingAuthenticationFacebookDto} from "./SiteSettingAuthenticationFacebookDto";
import {SiteSettingAuthenticationGoogleDto} from "./SiteSettingAuthenticationGoogleDto";
import {SiteSettingAuthenticationMicrosoftDto} from "./SiteSettingAuthenticationMicrosoftDto";
import {SiteSettingAuthenticationTwitterDto} from "./SiteSettingAuthenticationTwitterDto";

export interface SiteSettingAuthenticationDto {
    facebook: SiteSettingAuthenticationFacebookDto;
    google: SiteSettingAuthenticationGoogleDto;
    microsoft: SiteSettingAuthenticationMicrosoftDto;
    twitter: SiteSettingAuthenticationTwitterDto;
}
