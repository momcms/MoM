export interface ExtensionInfo {
        name: string;
        description: string;
        authors: string;
        iconCss: string;
        type: string;
        versionMajor: number;
        versionMinor: number;
        versionPatch: number;
        dependsOn: [ExtensionInfo];
}