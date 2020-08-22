import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Review } from '../models/review';

@Injectable({
  providedIn: 'root'
})

export class ReviewService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createReview(review: Review) {
    return this.http.post(this.baseUrl + 'review', review);
  }
}
