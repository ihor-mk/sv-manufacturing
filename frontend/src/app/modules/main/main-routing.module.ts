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
                loadChildren: () => import('../live-view/live-view.module')
                    .then((m) => m.LiveViewModule)
            },

            {
                path: 'history',
                loadChildren: () => import('../history/history.module')
                    .then((m) => m.HistoryModule)
            },
            
            {
                path: 'records',
                loadChildren: () => import('../records/records.module')
                    .then((m) => m.RecordsModule)
            },

            {
                path: 'rating',
                loadChildren: () => import('../rating/rating.module')
                    .then((m) => m.RatingModule)
            },
            {
                path: '**',
                redirectTo: '/live',
                pathMatch: 'full'
            }

        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MainRoutingModule { }