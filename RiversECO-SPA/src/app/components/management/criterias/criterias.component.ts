import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { CriteriaModalComponent } from './criteria-modal/criteria-modal.component';
import { CriteriaService } from 'src/app/services/criteria.service';
import { Criteria } from 'src/app/models/criteria';

@Component({
  selector: 'app-criterias',
  templateUrl: './criterias.component.html',
  styleUrls: ['./criterias.component.css']
})

export class CriteriasComponent implements OnInit {
  criterias: Criteria[];
  bsModalRef: BsModalRef;

  constructor(
    private route: ActivatedRoute,
    private criteriaService: CriteriaService,
    private modalService: BsModalService) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.criterias = data['criterias'];
    });
  }

  createCriteria() {
    const options = {
      initialState: {
        criteria: undefined
      },
      animated: true,
      class: 'modal-window'
    };

    this.bsModalRef = this.modalService.show(CriteriaModalComponent, options);
  }

  updateCriteria(criteria: Criteria) {
    const options = {
      initialState: {
        criteria: criteria
      },
      animated: true,
      class: 'modal-window'
    };

    this.bsModalRef = this.modalService.show(CriteriaModalComponent, options);
  }
}
