import {UserDto[]} from "./UserDto";

export interface RoleDto {
    id: string;
    name: string;
    concurrencyStamp: string;
    normalizedName: string;
    users: UserDto[];
}
