import { CommonModule } from "@angular/common";

import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { CanjearComponent } from "./components/canjear/canjear.component";
import { CrearComponent } from "./components/crear/crear.component";
import { PromocionRoutingModule } from "./promocion-routing.module";
import { PromocionComponent } from "./promocion.component";
import { ListaComponent } from "./components/lista/lista.component";

@NgModule({
  declarations: [PromocionComponent, CrearComponent,CanjearComponent, ListaComponent],
  imports: [
    CommonModule,
    PromocionRoutingModule,
    ReactiveFormsModule,

  ],
  exports: []
})
export class PromocionModule { }
