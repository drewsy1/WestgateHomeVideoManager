import { ISourceFormat } from '../interfaces/ISourceFormat';

export class SourceFormat implements ISourceFormat {
    sourceFormatId;
    sourceFormatName;

    static sourceFormatFromId(
        sourceFormatId: number,
        sourceFormatCollection: ISourceFormat[]
    ) {
        return sourceFormatCollection.find(
            format => format.sourceFormatId === sourceFormatId
        );
    }
}
