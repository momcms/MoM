import {SiteSettingDto} from "./SiteSettingDto";

export interface SiteSettingInstallationStatusDto {
    siteSetting: SiteSettingDto;
    installationResultCode: string;
    message: string;
    installationStatus: InstallationStatus;
}
