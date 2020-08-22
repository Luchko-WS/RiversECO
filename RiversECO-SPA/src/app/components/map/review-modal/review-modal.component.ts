import { Component, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { WaterObject } from 'src/app/models/water-object';
import { Criteria, CheckedCriteria } from 'src/app/models/criteria';
import { CriteriaService } from 'src/app/services/criteria.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-modal',
  templateUrl: './review-modal.component.html',
  styleUrls: ['./review-modal.component.css']
})

export class ReviewModalComponent implements OnInit {
  object: WaterObject;
  criterias: CheckedCriteria[];
  author: string;
  comment: string;

  constructor(
    private criteriaService: CriteriaService,
    private reviewService: ReviewService,
    public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.criteriaService.getCriterias()
      .subscribe((res: Criteria[]) => {
        this.criterias = res.map(criteria => {
          return {
            id: criteria.id,
            name: criteria.name,
            description: criteria.description,
            checked: false
          };
        });
      }, error => {
        console.error(error);
      });
  }

  checkCriteria(criteria: CheckedCriteria) {
    criteria.checked = !criteria.checked;
  }

  validateReview() {
    return !this.isStringEmptyOrWhitespaces(this.author) &&
      !this.isStringEmptyOrWhitespaces(this.comment);
  }

  isStringEmptyOrWhitespaces(value: string) {
    if (!value || value.trim() === '') {
      return true;
    }

    return false;
  }

  submitReview() {
    const filteredCriterias = this.criterias.filter(criteria => {
      return criteria.checked;
    });

    const review = {
      createdBy: this.author,
      comment: this.comment,
      criterias: filteredCriterias,
      waterObjectId: this.object.id
    };

    this.reviewService.createReview(review)
      .subscribe(review => {
          console.log('Review created.', review);
      }, error => {
          console.error(error);
      });
  }
}
