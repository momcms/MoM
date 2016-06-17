import {RoleDto} from "./RoleDto";

export interface UserDto {
    id: string;
    userName: string;
    email: string;
    emailConfirmed: boolean;
    accessFailedCount: number;
    concurrencyStamp: string;
    lockoutEnabled: boolean;
    lockoutEnd: Date;
    phoneNumber: string;
    phoneNumberConfirmed: boolean;
    twoFactorEnabled: boolean;
    roles: RoleDto[];
}
