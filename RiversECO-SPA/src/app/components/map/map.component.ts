import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import Map from 'ol/Map';
import View from 'ol/View';
import VectorLayer from 'ol/layer/Vector';
import Style from 'ol/style/Style';
import Icon from 'ol/style/Icon';
import OSM from 'ol/source/OSM';
import {defaults as defaultFormat, GeoJSON} from 'ol/format';
import {Point, Polygon} from 'ol/geom';
import {defaults as defaultInteraction, Select} from 'ol/interaction';
import {defaults as defaultSource, Vector} from 'ol/source';
import Feature from 'ol/feature';
import * as olProj from 'ol/proj';
import {defaults as defaultControls, Rotate, ScaleLine, MousePosition} from 'ol/control';
import {default as defaultOverlay} from 'ol/overlay';
import TileLayer from 'ol/layer/Tile';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { ReviewModalComponent } from '../review-modal/review-modal.component';
import { WaterObject } from 'src/app/models/water-object';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {
  // members related to the UI map element
  private map: any;
  private overlay: any;
  private selectInteraction: Select;

  selectedObject: WaterObject;
  waterObjects: WaterObject[];
  bsModalRef: BsModalRef;

  constructor(private route: ActivatedRoute, private modalService: BsModalService) { }

  ngOnInit() {
    this.initMap();
    this.initOverlayWindow();

    this.route.data.subscribe(data => {
      this.waterObjects = data['waterObjects'];
      this.drawFeatures(this.waterObjects);
    });
  }

  initMap() {
    this.map = new Map({
      target: 'map-layer',
      controls: this.adjustControls(),
      layers: this.adjustLayers(),
      interactions: this.adjustInteractions(),
      view: this.adjustView()
    });
  }

  adjustControls(): any {
    return defaultControls({attribution: false, zoom: true}).extend([
      new Rotate(),
      new ScaleLine(),
      new MousePosition()
    ]);
  }

  adjustLayers(): any[] {
    return [
      new TileLayer({
        source: new OSM()
      })
    ];
  }

  adjustInteractions(): any {
    const selectInteraction = new Select({multi: false});
    selectInteraction.on('select', e => {
      if (e.selected.length !== 1) {
        this.closeOverlay();
        return;
      }

      const mouseCoordinates = this.getCurrentMouseCoordinates();

      const selectedFeatureValues = e.selected[0].values_;
      this.selectedObject = {
        id: selectedFeatureValues.id,
        name: selectedFeatureValues.name_ukr,
        state: 15
      };

      this.overlay.setPosition(mouseCoordinates);
    });

    this.selectInteraction = selectInteraction;
    return defaultInteraction().extend([this.selectInteraction]);
  }

  getCurrentMouseCoordinates() {
    const mouseCoordinates = document
      .getElementsByClassName('ol-mouse-position')[0]
      .innerHTML.split(',');
    return mouseCoordinates;
  }

  adjustView(): View {
    return new View({
      projection: 'EPSG:3857',
      center: olProj.fromLonLat([28.5785, 49.2]),
      zoom: 10
    });
  }

  initOverlayWindow() {
    const overlay = new defaultOverlay({
      element: document.getElementById('popup'),
      autoPan: true,
      autoPanAnimation: {
        duration: 250
      }
    });

    this.overlay = overlay;
    this.map.addOverlay(overlay);
  }

  closeOverlay() {
    this.selectInteraction.getFeatures().clear();
    this.overlay.setPosition(undefined);
  }

  drawFeatures(objects: WaterObject[]) {
    const lakesLayer = new VectorLayer({
      source: new Vector({
        url: '../../../assets/lakes.geojson',
        format: new GeoJSON()
      })
    });

    const riversLayer = new VectorLayer({
      source: new Vector({
        url: '../../../assets/rivers.geojson',
        format: new GeoJSON()
      })
    });

    this.map.addLayer(lakesLayer);
    this.map.addLayer(riversLayer);
  }

  createReivew() {
    this.closeOverlay();

    const initialState = {
      object: this.selectedObject,
      criterias: null
    };
    this.bsModalRef = this.modalService.show(ReviewModalComponent, {initialState});
  }
}
