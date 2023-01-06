import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreOffersPageComponent } from './ire-offers-page.component';

describe('IreOffersPageComponent', () => {
  let component: IreOffersPageComponent;
  let fixture: ComponentFixture<IreOffersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreOffersPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreOffersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
