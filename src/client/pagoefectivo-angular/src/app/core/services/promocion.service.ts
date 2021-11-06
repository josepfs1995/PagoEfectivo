import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CanjearPromocionModel } from '../models/canjear-promocion.model';
import { CrearPromocionModel } from '../models/crear-promocion.model';
import { PromocionModel } from '../models/promocion.model';

@Injectable({
  providedIn: 'root',
})
export class PromocionService {
  constructor(private http: HttpClient) {}
  get(): Observable<PromocionModel[]> {
    return this.http.get<PromocionModel[]>(
      `${environment.servicio}/promocion`
    );
  }
  crear(promocion: CrearPromocionModel): Observable<CrearPromocionModel> {
    return this.http.post<CrearPromocionModel>(
      `${environment.servicio}/promocion`,
      promocion
    );
  }
  canjear(promocion: CanjearPromocionModel): Observable<CrearPromocionModel> {
    return this.http.post<CrearPromocionModel>(
      `${environment.servicio}/promocion/canjear`,
      promocion
    );
  }
}
