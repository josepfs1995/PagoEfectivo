import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { PromocionModel } from "src/app/core/models/promocion.model";
import { PromocionService } from "src/app/core/services/promocion.service";

@Component({
    selector: 'app-lista',
    templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {
  promociones: PromocionModel[] = [];
  constructor(private promocionService:PromocionService) {

  }
  ngOnInit(): void {
    this.promocionService.get().subscribe(resp => {
      this.promociones = resp;
    });
  }

}
