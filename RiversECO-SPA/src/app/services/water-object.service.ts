import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Criteria } from '../models/criteria';
import { WaterObject } from '../models/water-object';

@Injectable({
  providedIn: 'root'
})

export class WaterObjectService {
  constructor(private http: HttpClient) { }

  getWaterObjects(): Observable<WaterObject[]> {
    return of<WaterObject[]>([
      {
          id: 'id1',
          name: 'name 1',
          state: 1
      },
      {
          id: 'id2',
          name: 'name 2',
          state: 2
      }
    ]);
  }
}
