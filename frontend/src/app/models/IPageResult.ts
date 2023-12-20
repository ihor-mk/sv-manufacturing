export interface IPageResult<T> {
    count: number
    pageIndex: number
    pageSize: number
    items: T[]
}