export interface ILiveViewCountsDto {
    LineId?: number,
    LineTitle?: string,
    ProductivityCurrent?: number,
    ProductivityTop?: number,
    ProductivityAvg?: number
    NomenclatureTitle?: string
    QuantityPlan?: number,
    QuantityFact?: number,
    StartedAt?: Date,
    FinishedAt?: Date,
    WorkTime?: number,
    IsNewNomenclature?: boolean,
    IsNewPrinterNomenclature?: boolean,
    IsPrinterOffline?: boolean
}