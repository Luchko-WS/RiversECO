import { Component, OnInit } from '@angular/core';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { CriteriaModalComponent } from './criteria-modal/criteria-modal.component';
import { CriteriaService } from 'src/app/services/criteria.service';
import { UtilsService } from 'src/app/services/utils.service';
import { Criteria, CheckedCriteria } from 'src/app/models/criteria';

@Component({
  selector: 'app-criterias',
  templateUrl: './criterias.component.html',
  styleUrls: ['./criterias.component.css']
})

export class CriteriasComponent implements OnInit {
  criterias: CheckedCriteria[];
  bsModalRef: BsModalRef;
  isLoaded: boolean;

  constructor(
    private criteriaService: CriteriaService,
    private utilsService: UtilsService,
    private modalService: BsModalService) {}

  ngOnInit() {
    this.isLoaded = false;
    this.criteriaService.getCriterias().subscribe(data => {
      this.criterias = data.map(criteria => {
        return {
          id: criteria.id,
          name: criteria.name,
          description: criteria.description,
          checked: false
        };
      });
      this.isLoaded = true;
    }, error => {
      console.error(error);
    });
  }

  createCriteria() {
    this.openCriteriaModal(undefined);
  }

  updateCriteria(criteria: Criteria) {
    this.openCriteriaModal(criteria);
  }

  openCriteriaModal(criteria: Criteria) {
    const options = {
      initialState: {
        criteria: criteria
      },
      animated: true,
      class: 'modal-window'
    };

    this.bsModalRef = this.modalService.show(CriteriaModalComponent, options);
    this.bsModalRef.content.resultCriteria.subscribe((result) => {
      if (!criteria) {
        this.criterias.push(result);
      } else {
        this.criterias.map(obj => result.id === obj.id ? result : obj);
      }

      this.ngOnInit();
    });
  }

  deleteCriterias() {
    const ids = this.utilsService.getSelectedCriterias(this.criterias).map(criteria => criteria.id);
    if (ids.length === 0) {
      return;
    }

    this.criteriaService.deleteCriterias(ids).subscribe(data => {
      this.ngOnInit();
    }, error => {
      console.error(error);
    });
  }
}
