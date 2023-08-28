import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';

const routes: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                component: MainPageComponent
            },
            {
                path: 'live',
                loadChildren: () => import('../live-veiw/live-veiw.module')
                    .then((m) => m.LiveVeiwModule)
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MainRoutingModule { }