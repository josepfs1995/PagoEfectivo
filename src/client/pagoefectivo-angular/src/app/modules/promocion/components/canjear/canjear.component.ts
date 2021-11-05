import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { PromocionService } from "src/app/core/services/promocion.service";

@Component({
    selector: 'app-canjear',
    templateUrl: './canjear.component.html'
})
export class CanjearComponent {
  formPromocion: FormGroup;
  constructor(private fb:FormBuilder,
    private promocionService:PromocionService,
    private toastr: ToastrService) {
    this.formPromocion = this.fb.group({
      codigo: ['', Validators.required]
    });
  }
  onSubmit() {
    if(this.formPromocion.valid) {
      this.promocionService.canjear(this.formPromocion.value).subscribe(
        data => {
          this.toastr.success('Promoción canjeada con éxito');
        },
        error => {
          error.error.errores.forEach((mensaje: any) => {
            this.toastr.error(mensaje);
          });
        }
      );
    }
  }
}
