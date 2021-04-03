import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { CriteriaModalComponent } from './criteria-modal/criteria-modal.component';
import { CriteriaService } from 'src/app/services/criteria.service';
import { UtilsService } from 'src/app/services/utils.service';
import { Criteria, CheckedCriteria } from 'src/app/models/criteria';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-criterias',
  templateUrl: './criterias.component.html',
  styleUrls: ['./criterias.component.css']
})

export class CriteriasComponent implements OnInit {
  criterias: CheckedCriteria[];
  bsModalRef: BsModalRef;
  isLoaded: boolean;

  dataSource: MatTableDataSource<CheckedCriteria>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private criteriaService: CriteriaService,
    private alertifyService: AlertifyService,
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

      // init table
      this.dataSource = new MatTableDataSource(this.criterias);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.dataSource.sortingDataAccessor = (data, sortHeaderId) => data[sortHeaderId].toLocaleLowerCase();
      this.dataSource.filterPredicate = (data: Criteria, filter: string) => {
        if (data.name.toLowerCase().includes(filter.toLowerCase())) {
          return true;
        } else {
          return false;
        }
      };

      this.isLoaded = true;
    }, error => {
      this.alertifyService.error('Не вдалося отримати критерії.');
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

    this.alertifyService.confirm('Видалення критеріїв', 'Ви впевнені, що бажаєте видалити обрані критерії?', () => {
      this.criteriaService.deleteCriterias(ids).subscribe(data => {
        this.alertifyService.success('Критерії видалено!');
        this.ngOnInit();
      }, error => {
        this.alertifyService.error('Під час видалення критеріїв виникла помилка.');
        console.error(error);
      });
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
