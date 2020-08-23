import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

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

  getReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(this.baseUrl + 'review');
  }

  getReviewsForWaterObject(waterObjectId: string): Observable<Review> {
    return this.http.get<Review>(this.baseUrl + 'review/' + waterObjectId);
  }
}
