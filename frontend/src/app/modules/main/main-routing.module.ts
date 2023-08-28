import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main-page/main-page.component';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        children: [
            {
                path: 'live',
                loadChildren: () => import('../live-veiw/live-veiw.module')
                    .then((m) => m.LiveVeiwModule)
            },
            {
                path: 'history',
                loadChildren: () => import('../history/history.module')
                .then((m) => m.HistoryModule)
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MainRoutingModule { }