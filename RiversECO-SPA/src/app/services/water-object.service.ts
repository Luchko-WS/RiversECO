import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WaterObject } from '../models/water-object';

@Injectable({
  providedIn: 'root'
})

export class WaterObjectService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getWaterObjects(): Observable<WaterObject[]> {
    return this.http.get<WaterObject[]>(this.baseUrl + 'waterobject');
  }

  getWaterObject(id: string): Observable<WaterObject> {
    return this.http.get<WaterObject>(this.baseUrl + 'waterobject/' + id);
  }
}
