import { NgModule } from '@angular/core';

import { TestRoutingModule } from './test-routing.module';
import { EditorModule } from 'primeng/editor';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [],
  imports: [TestRoutingModule, EditorModule, FormsModule, ButtonModule],
})
export class TestModule {}
