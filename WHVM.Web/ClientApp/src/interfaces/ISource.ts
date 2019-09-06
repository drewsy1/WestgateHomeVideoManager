export interface ISource {
    sourceId: number;
    sourceName: string;
    sourceDateCreated: string;
    sourceDateImported: string;
    sourceFormatId: number;
    sourceFilePath: string;
    files: [];
    sourceDateStart: string;
    sourceDateEnd: string;

    [key: string]: any;
}
