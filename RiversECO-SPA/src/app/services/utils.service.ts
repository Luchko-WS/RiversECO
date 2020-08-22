import { Injectable } from '@angular/core';

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
}