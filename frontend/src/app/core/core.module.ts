import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [HttpClientModule, SharedModule],
})
export class CoreModule {}