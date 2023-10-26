import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { RecordsPageComponent } from "./records-page/records-page.component";

const routes: Routes = [
    { path: '', component: RecordsPageComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class RecordsRoutingModule {}