import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrearPromocionModel } from 'src/app/core/models/crear-promocion.model';
import { PromocionService } from 'src/app/core/services/promocion.service';

@Component({
  selector: 'app-crear',
  templateUrl: './crear.component.html',
})
export class CrearComponent {
  formPromocion: FormGroup;
  codigo: string = '';
  esSuccess: boolean = false;
  constructor(
    private fb: FormBuilder,
    private promocionService: PromocionService,
    private toastr: ToastrService
  ) {
    this.formPromocion = this.fb.group({
      nombre: ['Josep', Validators.required],
      email: [
        'josepfs_1995@hotmail.com',
        [Validators.required, Validators.email],
      ],
    });
  }
  onSubmit() {
    this.esSuccess = false;
    if (this.formPromocion.valid) {
      this.promocionService.crear(this.formPromocion.value).subscribe(
        (data: CrearPromocionModel) => {
          this.esSuccess = true;
          this.codigo = data.codigo || '';
          this.toastr.success('Promoción creada correctamente.');
        },
        (error) => {
          error.error.errores.forEach((mensaje: any) => {
            this.toastr.error(mensaje);
          });

        }
      );
    }
  }
}
