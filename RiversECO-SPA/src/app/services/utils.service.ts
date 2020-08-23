import { Injectable } from '@angular/core';
import { CheckedCriteria } from '../models/criteria';

@Injectable({
  providedIn: 'root'
})

export class UtilsService {

  constructor() { }

  isStringEmptyOrWhitespaces(value: string): boolean {
    if (!value || value.trim() === '') {
      return true;
    }

    return false;
  }

  getSelectedCriterias(criterias: CheckedCriteria[]) {
    const result = criterias.filter(criteria => {
      return criteria.checked;
    });
    return result;
  }
}
