import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CanjearComponent } from './components/canjear/canjear.component';
import { CrearComponent } from './components/crear/crear.component';
import { ListaComponent } from './components/lista/lista.component';
import { PromocionComponent } from './promocion.component';

const routes: Routes = [{
  path: '',
  component: PromocionComponent,
  children: [
    {
      path: '',
      redirectTo: 'lista',
      pathMatch: 'full'
    },
    {
      path: 'lista',
      component: ListaComponent
    },
    {
      path: 'crear',
      component: CrearComponent
    },
    {
      path: 'canjear',
      component: CanjearComponent
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PromocionRoutingModule { }
