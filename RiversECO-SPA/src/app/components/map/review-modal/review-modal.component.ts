import { Component, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { WaterObject } from 'src/app/models/water-object';
import { Criteria, CheckedCriteria } from 'src/app/models/criteria';
import { CriteriaService } from 'src/app/services/criteria.service';
import { stringify } from 'querystring';

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

  constructor(private criteriaService: CriteriaService, public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.author = 'Andrii Luchko';

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
      author: this.author,
      criterias: filteredCriterias,
      comment: this.comment
    };

    console.log(review);
  }
}
