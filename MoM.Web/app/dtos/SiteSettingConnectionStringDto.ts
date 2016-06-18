
export interface SiteSettingConnectionStringDto {
    server: string;
    database: string;
    useWindowsAuthentication: boolean;
    username: string;
    password: string;
    installationStatus: InstallationStatus;
}
