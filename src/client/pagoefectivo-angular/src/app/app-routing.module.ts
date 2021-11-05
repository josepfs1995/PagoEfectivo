import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'promocion',
  },
  {
    path: 'promocion',
    loadChildren: () =>
      import('./modules/promocion/promocion.module').then(
        (m) => m.PromocionModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
