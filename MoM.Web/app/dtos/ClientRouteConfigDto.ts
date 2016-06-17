
export interface ClientRouteConfigDto {
    clientRouteConfigId: number;
    name: string;
    displayName: string;
    path: string;
    component: string;
    importPath: string;
    useAsDefault: boolean;
    sortOrder: number;
    type: ClientRouteConfigType;
    iconClass: string;
}
