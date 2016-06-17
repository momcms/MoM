
export interface SiteSettingEmailDto {
    hostName: string;
    password: string;
    port: number;
    requireCredentials: boolean;
    senderEmailAdress: string;
    userName: string;
    useSSL: boolean;
}
