import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LiveVeiwPageComponent } from './live-veiw-page/live-veiw-page.component';

const routes: Routes = [
    { path: '', component: LiveVeiwPageComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class LiveVeiwRoutingModule {}