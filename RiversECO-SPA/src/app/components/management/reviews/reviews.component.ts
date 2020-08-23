import { Component, OnInit } from '@angular/core';

import { ReviewService } from 'src/app/services/review.service';
import { Review } from 'src/app/models/review';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})

export class ReviewsComponent implements OnInit {
  reviews: Review[];
  isLoaded: boolean;

  constructor(
    private reviewService: ReviewService) {}

  ngOnInit() {
    this.isLoaded = false;
    this.reviewService.getReviews().subscribe(data => {
      this.reviews = data;
      this.isLoaded = true;
    }, error => {
      console.error(error);
    });
  }
}
