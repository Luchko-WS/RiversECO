import { Component, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { WaterObject } from 'src/app/models/water-object';
import { Criteria, CheckedCriteria } from 'src/app/models/criteria';
import { CriteriaService } from 'src/app/services/criteria.service';
import { ReviewService } from 'src/app/services/review.service';
import { UtilsService } from 'src/app/services/utils.service';

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

  isLoaded: boolean = false;
  filteredCriterias: CheckedCriteria[] = [];
  criteriaFilterText: string;

  constructor(
    private criteriaService: CriteriaService,
    private reviewService: ReviewService,
    private utilsService: UtilsService,
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
        this.filterCriterias();
      }, error => {
        this.isLoaded = true;
        console.error(error);
      });
  }

  filterCriterias() {
    this.isLoaded = false;
    this.filteredCriterias = this.criterias.filter(
      criteria => {
        if (!this.criteriaFilterText) {
          return true;
        }     
        return criteria.name.toLowerCase().includes(this.criteriaFilterText.toLowerCase())
      });
    this.isLoaded = true;
  }

  checkCriteria(criteria: CheckedCriteria) {
    criteria.checked = !criteria.checked;
  }

  validateReview() {
    return !this.utilsService.isStringEmptyOrWhitespaces(this.author) &&
      !this.utilsService.isStringEmptyOrWhitespaces(this.comment);
  }

  submitReview() {
    const filteredCriterias = this.utilsService.getSelectedCriterias(this.criterias);

    const review = {
      createdBy: this.author,
      comment: this.comment,
      criterias: filteredCriterias,
      waterObjectId: this.object.id
    };

    this.reviewService.createReview(review)
      .subscribe(review => {
          console.log('Review created.', review);
          this.bsModalRef.hide();
      }, error => {
          console.error(error);
      });
  }
}
