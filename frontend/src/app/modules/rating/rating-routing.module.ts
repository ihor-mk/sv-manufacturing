import { RouterModule, Routes } from "@angular/router";
import { RatingPageComponent } from "./rating-page/rating-page.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
    { path: '', component: RatingPageComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class RatingRoutingModule {}