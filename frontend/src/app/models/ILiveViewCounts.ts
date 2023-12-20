export interface ILiveViewCounts {
    lineId?: number,
    lineTitle?: string,
    productivityCurrent?: number,
    productivityTop?: number,
    productivityAvg?: number
    nomenclatureTitle?: string
    quantityPlan?: number,
    quantityFact?: number,
    startedAt?: Date,
    finishedAt?: Date
    workTime?: number,
    isNewNomenclature?: boolean,
    isNewPrinterNomenclature?: boolean,
}