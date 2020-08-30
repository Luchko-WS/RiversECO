import { Component, OnInit, ViewChild } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

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

  dataSource: MatTableDataSource<Review>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private reviewService: ReviewService) {}

  ngOnInit() {
    this.isLoaded = false;
    this.reviewService.getReviews().subscribe(data => {
      this.reviews = data;

      // init table
      this.dataSource = new MatTableDataSource(this.reviews);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.dataSource.sortingDataAccessor = (data, sortHeaderId) => {
        switch (sortHeaderId) {
          case 'waterObject': return data.waterObject.name;
          default: return data[sortHeaderId];
        }
      };

      this.isLoaded = true;
    }, error => {
      console.error(error);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
