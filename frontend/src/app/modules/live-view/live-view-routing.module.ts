import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LiveViewPageComponent } from './live-view-page/live-view-page.component';

const routes: Routes = [
    { path: '', component: LiveViewPageComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class LiveViewRoutingModule {}